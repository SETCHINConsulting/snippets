        /// <summary>
        /// Formats the error message to show stack trace of inner exceptions.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="indent">The indent.</param>
        /// <returns></returns>
        public static string FormatErrorMessage(Exception exception, string indent = "")
            {
            string errorMessage = string.Empty;
            string newLine = "\n";
            string stars = new string('*', 80);
            errorMessage = indent + stars + newLine;
            errorMessage += indent + exception.GetType().Name + ": \"" + exception.Message + "\"" + newLine;
            errorMessage += indent + new string('-', 80) + newLine;

            try
                {
                if (exception.InnerException != null)
                    {
                    errorMessage += indent + "InnerException:" + newLine;
                    errorMessage += FormatErrorMessage(exception.InnerException, indent + "   ");
                    }

                if (exception.StackTrace != null)
                    {
                    foreach (string line in exception.StackTrace.Split(new string[] { " at " }, StringSplitOptions.RemoveEmptyEntries))
                        {
                        if (string.IsNullOrEmpty(line.Trim())) continue;
                        string[] parts;
                        parts = line.Trim().Split(new string[] { " in " }, StringSplitOptions.RemoveEmptyEntries);
                        string class_info = parts[0];
                        if (parts.Length == 2)
                            {
                            parts = parts[1].Trim().Split(new string[] { "line" }, StringSplitOptions.RemoveEmptyEntries);
                            string src_file = parts[0];
                            int line_nr = int.Parse(parts[1]);
                            errorMessage += indent + "  " +src_file.TrimEnd(':') + "(" + line_nr + ",1):   " + class_info + newLine;
                            }
                        else
                            {
                            errorMessage += indent + "  " + class_info + newLine;
                            }
                        }
                    }

                errorMessage += indent + stars + newLine;
                }
            catch (Exception ex)
                {
                Trace.WriteLine(ex.Message);
                }

            return errorMessage;
            }
