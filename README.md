# Folder Synchronization

### Purpose of the Program
The program is aimed at synchronizing files and directories from a source location to a destination at specified intervals. It maintains logs of these synchronization activities.

### Handling Command-Line Arguments
The code starts by checking the number of **command-line arguments** provided when executing the program. Depending on the number of arguments (0 to 4), different messages are displayed to guide the user on how to provide the required input arguments.

### Main Execution Flow
If the user provides all required arguments (4), the `executeCode()` function is called to initiate the synchronization process.
- Inside the `executeCode()` function the provided command-line arguments are extracted, including the source path, destination path, log path, and synchronization interval.
- A `Logger` instance is created to handle logging messages to both the console and a log file.
- Two `ItemList` instances, `sourceItemList` and `destinationItemList`, are created to store lists of items (files and directories) in the source and destination paths.
- The `GetAllFilesAndDirectories()` method is called on both `sourceItemList` and `destinationItemList` to populate them with respective items.
- The `CompareLists()` method is called to compare the contents of the source and destination item lists and synchronize them.
- A timer is set up using the `timer()` function, which periodically triggers the synchronization process based on the specified interval.

### Synchronization Process (`ItemList` and `Item` Classes)
The `ItemList` class is an extension of the `List<Item>` class with additional methods for handling item comparison and synchronization.
The `CompareLists()` method in the `ItemList` class performs the following tasks:
- Compares items in the source list with those in the destination list to determine which items need to be added, updated, or deleted.
- Any items not verified during the comparison are marked for synchronization.
- Files that exist in the destination but not in the source are deleted from the destination.
- Directories in the destination that are not present in the source are also deleted.
Files in the source that are different from those in the destination (based on name and hash) are copied to the destination.
- The `Item` class represents individual files and holds information about their path, name, directory, hash, and verification status.

### Logging (`Logger` Class)
The `Logger` class handles logging messages both to the console and a log file. It ensures that log messages are timestamped and formatted consistently. It also creates the log file if it doesn't exist and adds a separator at the beginning for better organization.

### Overall Flow Control
The code demonstrates a clear separation of concerns through various classes:

`ItemList` and `Item` handle file and directory management and comparison.
`Logger` manages logging operations.
The main execution flow `executeCode()` orchestrates the entire synchronization process.
The use of a timer ensures that the synchronization process repeats at specified intervals, enhancing the program's usability for continuous synchronization.

### Technical Limitations
Currently, the program correctly copies files located within directories but does not copy the directories themselves.

The current implementation focuses on synchronizing individual files within the source and destination paths but does not handle the synchronization of entire directories. Therefore, directories present within the source path are not being recreated in the destination path.

To address this issue and ensure that directories are copied along with their contents, adjustments are needed in the synchronization process.
