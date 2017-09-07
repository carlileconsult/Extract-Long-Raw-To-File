# Extract-Long-Raw-To-File
Simple console application that extracts a long raw from oracle and writes to a file

This Visual Studio 2013 application was created to extract a long raw rtf file from an oracle database, and write it to a file out on a directory. To properly run the application you will need to alter the app.config file with your desired parameters, or you can just grab some of the code from the program.cs and add it to your own project or solution.

App.Config File Parameters:
===========================

DBConnection - The connection string for the oracle database

DBCommand - The SQL statement that will be executed

DBFileNameColumn - The name of the file that is being extracted

DBLongRawColumn - The actual long raw column that is storing the document

OutputFolder - The main folder to write all files

