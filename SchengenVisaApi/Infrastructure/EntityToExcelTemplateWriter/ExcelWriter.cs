using System.Reflection;
using System.Text;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Infrastructure.EntityToExcelTemplateWriter
{
    /// <summary>
    /// Writes object to excel using template.xlsx file and reflections
    /// </summary>
    public class ExcelWriter : IEntityWriter
    {
        private const char InsertionSymbol = '$';
        private readonly char[] endChars = [',', ';'];

        /// <summary>
        /// Write object to stream in Excel table format
        /// </summary>
        /// <param name="entity"> object to write </param>
        /// <param name="cancellationToken"> cancellation token </param>
        /// <returns> Stream with template.xlsx file with replaced entries like '$EntityPropName.AnotherProp' </returns>
        /// <exception cref="NullReferenceException"> thrown when template file is incorrect </exception>
        /// <exception cref="InvalidOperationException"> thrown if any property path in template is incorrect</exception>
        public async Task<Stream> WriteEntityToStream(object entity, CancellationToken cancellationToken)
        {
            var outStream = new MemoryStream();
            await using (var stream = File.Open("template.xlsx", FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(outStream, cancellationToken);
            }

            using var spreadsheetDocument = SpreadsheetDocument.Open(outStream, true);

            var workbookPart = spreadsheetDocument.WorkbookPart
                               ?? throw new NullReferenceException("There is no workbook part in document");
            var shareStringTable = workbookPart.SharedStringTablePart?.SharedStringTable ??
                                   throw new NullReferenceException("There is no data in document");
            var shareStringTableItems = shareStringTable.Elements<SharedStringItem>().ToArray();

            foreach (var item in shareStringTableItems)
            {
                if (string.IsNullOrEmpty(item.InnerText))
                {
                    continue;
                }

                var entries = item.InnerText.Split();
                for (var i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];
                    if (entry.FirstOrDefault() is not InsertionSymbol || entry.Length <= 1)
                    {
                        continue;
                    }

                    entry = entry[1..];
                    var trimmedCount = entry.Length - entry.TrimEnd(endChars).Length;
                    var trimmed = entry[^trimmedCount..];
                    entry = entry.TrimEnd(endChars);

                    var memberPath = entry.Split('.');

                    var value = GetValueFor(entity, memberPath.First());
                    var stringToInsert = "None";
                    foreach (var memberName in memberPath.Skip(1))
                    {
                        if (value is null)
                        {
                            break;
                        }

                        value = GetValueFor(value, memberName);
                    }

                    if (value is not null)
                    {
                        switch (value)
                        {
                            case DateTime date:
                                stringToInsert = date.ToShortDateString();
                                break;
                            case Enum val:
                                var enumString = val.ToString();
                                var stringBuilder = new StringBuilder();
                                for (var charIndex = 0; charIndex < enumString.Length - 1; charIndex++)
                                {
                                    stringBuilder.Append(enumString[charIndex]);
                                    if (char.IsUpper(enumString[charIndex + 1]))
                                    {
                                        stringBuilder.Append(' ');
                                    }
                                }

                                stringBuilder.Append(enumString.Last());

                                stringToInsert = stringBuilder.ToString();
                                break;
                            default:
                                stringToInsert = value.ToString();
                                break;
                        }
                    }

                    entries[i] = stringToInsert! + trimmed;
                }

                item.Text!.Text = string.Join(' ', entries);
            }

            spreadsheetDocument.Save();
            return outStream;
        }

        private static object? GetValueFor(object entity, string member)
        {
            var memberInfo = entity.GetType()
                                 .GetMembers()
                                 .FirstOrDefault(p => p.Name == member)
                             ?? throw new InvalidOperationException(
                                 $"Invalid member path in document. Not found: {member}");
            return memberInfo switch
            {
                PropertyInfo propertyInfo => propertyInfo.GetValue(entity),
                MethodInfo methodInfo => methodInfo.Invoke(entity, []),
                _ => throw new InvalidOperationException("Only properties and methods allowed.")
            };
        }
    }
}
