# R5T.NetStandard.IO.Paths
A path handling library.

## Goals

* Allow simple, easy, and fast path creation and manipulation with no questions about correctness of results.
* Better communicate function intent using strongly-typed instead of stringly-typed path-types (for example, file-path as a different type from file-name instead of both being strings).
* Create base-types for more derived path-types that can still be manipulated as paths by base-type functionality (for example, project file-name and solution file-name can both be suffixed to a directory-path to get a file-path).
* Allow manipulation of both Windows and non-Windows paths on either system.
* Allow better manipulation of stringly-typed path-types.
* Critique and add documentation to the System.IO.Path functionality.

## Design
All paths take the form:

	[root]{optional, {directory separator}zero or more separated directory names}{optional, {directory separator}file name}

Any path that beings with a root is a rooted, or absolute, path. Thus all paths take the form:

	[absolute path]{optional, {directory separator} zero or more separated directory names}{optional, {directory separator}file name}

Among the strongly-typed path types, everything is a PathSegment, except for:
	
* `AbsolutePath` and its derivatives, `DirectoryPath`, `FilePath`, and `GenericAbsolutePath`. An absolute-path is not a path-segment, it is a full path.
* `FileNameWithoutExtension`, `FileExtension`, and `GenericFileNameSegment`. These are file-name segments, and must first be combined into a file name, which is a path-segment, before being combined into a path.
* `GenericDirectoryNameSegment`. This is a directory-name segment, and must first be combined with other directory name segments into a directory name, which is a path-segment, before being combined into a path.

This means that all paths take the form:

	[absolute path]{optional, {directory separator} zero or more directory-separated path segments}.

This is the form used for all path combining operations. The result is an absolute-path, which, depending on context only known by the caller, can be cast to either a directory-path or file-path. Thus all paths take one of the forms:

	[absolute path]{optional, {directory separator} zero or more directory-separated **directory**-path segments}.
	[absolute path]{optional, {directory separator} zero or more directory-separated **file**path segments}.

Finally, path segments can include things like the current directory name ('.'), or the parent directory name ('..'), and can be combined into a relative path to either a directory or a file. Thus all paths take one of the forms:

	[absolute path]{optional, {directory separator} relative path to a directory}
	[absolute path]{optional, {directory separator} relative path to a file}

### Segments and Separators
Paths are made of path-segments separated by directory-separators. Path segments include roots (ex: /mnt, C:, E:), directory-names, and file-names.

Directory-names and file-names can themselves be composed of segments. For example, directory-names like "Documents-NEW" and "Documents-OLD" obviously communicate information in their segments, separated by a '-' separator. File-names are more obviously composed of segments; "temp.txt" includes the file-name without extension "temp" and the file-extension "txt" separated by the file-extension separator '.'.

### Path Types
All path-types derive from [TypedString], which allows strong-typing of "stringly"-typed data.

The inheritance hierarchy of path-types starts with the base-types:

* `AbsolutePath` - A rooted-path that can be directly used to access a resource.
* `PathSegment` - All other path-parts including directory-names, file-names, and relative-paths.

Since directories are different from files, `PathSegment` is sub-classed into:

* `DirectoryPathSegment` - Includes directory-names and relative-paths to directories.
* `FilePathSegment` - Includes file-names and relative-paths to files.

Note that `AbsolutePath` and `PathSegment` are abstract, meaning that instances of these types cannot be created. Instead, instances of derived-types must be created. This ensures that the extra context in which an absolute-path exists (for example, is it an absolute-path of a file or directory?) is captured in the name of derived type (`FilePath` or `DirectoryPath`).

Still, general concrete implementations are provided for two reasons. First, users might want to quickly use absolute-paths without creating their own derived types. Second, in fundamental path manipulations a context-free type is required. For example, combining an absolute-path with a path-segment produces an absolute-path, which may be a directory-path or file-path depending on context. This context is unknowable to the function performing the simple combination operation. These general implementations are:

* `GeneralAbsolutePath`
* `GeneralPathSegment`

The list of path-types is:

* `Root`
* `DirectoryName`
* `DirectoryPath`
* `DirectoryRelativePath`
* `FileName`
* `FilePath`
* `FileRelativePath`

### Path Separator Types

* `DirectorySeparator` - Separates path-segments (usually directory-names and the file-name) in the path. For example, '\' in "C:\Temp\temp.txt". Note that this value is different between Windows and non-Windows systems. Note that this is *NOT* the path-separator even though colloquially the directory-separator is often called the path-separator. The directory-separator separates directories (path-segments) in a path.
* `FileExtensionSeparator` - Separates the file-name without extension from the file-extension. For example, '.' in "C:\Temp\temp.txt".
* `VolumeSeparator` - Separates the volume part (drive, or root) from the rest of the path. For example, ':' in "C:\Temp\temp.txt".
* `PathSeparator` - Separates paths in a path aggregation (like the PATH environment variable). For example, ';' in "path1;path2;path3". Note that this *NOT* the directory-separator even though the directory-separator is colloquially called the path-separator. The path-separator separates paths.

### Name Segment Types
Directory-names and file-names can be composed of segments that can be combined to get a directory-name or file-name. Name segments begin with two types that inherit from [`TypedString`]:

* `DirectoryNameSegment`
* `FileNameSegment`

Note that `DirectoryNameSegment` and `FileNameSegment` are abstract, meaning that instances of these types cannot be created. Instead, instances of derived-types must be created. This ensures that the extra context carried by a directory-name segment (for example, whether it is NEW or OLD) is captured in the name of derived type (`AgeIndicatorDirectoryNameSegment`).

Still, general concrete implementations are provided for two reasons. First, users might want to quickly use directory-name and file-name segments without creating their own derived types. Second, in fundamental path manipulations a context-free type is required. For example, combining file-name segments together produces file-name segment, which may be of any "type" depending on context. This context is unknowable to the function performing the simple combination operation. These general implementations are:

* `GeneralDirectoryNameSegment`
* `GenerialFileNameSegment`

There are no directory-name or file-name segment types in the base library. This context is usually very application-specific.

### Name Segment Separator Types
Directory-name and file-name segments are separated by separators.

* `DirectoryNameSegmentSeparator` - Separates directory-name segments. For example, '-' in "C:\Temp-OLD\temp.txt". Note that this sepa
* `FileNameSegmentSeparator` - Separates file-name segments (like the file-name without extension and file-extension). For example, '.' in "C:\Temp\temp.txt".
* `FileExtensionSeparator` - This is the most famous `FileNameSegmentSeparator`, as it is used in nearly every file-name to separate the file-name without extension segment from the file-extension segment. For example, the '.' in "C:\Temp\temp.txt".

### String Extension Methods
"AsXXX" extension methods are provided to convert strings into the various path-types. This creates cleaner code than littering everywhere with "new Constructor()" calls. The "As" methods borrow from casting to indicate that no validation of string-value is performed; it is simply "cast" to the desired path-type.

### AbsolutePath and PathSegment Extension Methods
"AsXXX" extension methods are provided to convert base path-types to their contextual types.

## Validation
Note that **NO** validation is performed on the string-values given to the various path-types.

## Constants
A Constants class is provided to allow access to the default separator and directory name values.

## Utilities
A Utilities class is provided that performs all path manipulation operations.

It's recommended to use this `Utilities` class by aliasing it to be `PathUtilities`:

    using PathUtilities = R5T.NetStandard.IO.Paths.Utilities.
    
The utilities class contains methods for

* Wrapping the `System.IO.Path` methods, providing critique and extra documentation of their features.
* Manipulating path-parts as strings. Note that these "stringly"-typed methods are somewhat limited by the exact limitations strongly-typed path-parts were created to overcome.
* Manipulating strongly-typed path parts.

### Combine
The most overloaded utility method is `Combine()`, which is the primary operation performed on path-parts. By default `Combine()` does:

* Append all path-segments together. For example: "C:", "Temp", "temp.txt" to "C:\Temp\temp.txt".
* Resolves any relative-path directory-names. For example: "C:\Temp1\Temp2\..\..\Temp1.2\temp.txt" to "C:\Temp1.2\temp.txt".
* Converts directory-separators to those of the desired platform. For example "C:/Temp/temp.txt" (non-Windows) to "C:\Temp\temp.txt" (Windows).

Most often, a user has various path-parts and wants to just combine them into a path. This operation is surprisingly harder than it looks! Some difficulties stem from the fact the `System.IO.Path.Combine()` method is just broken; if any of the path-segments begins with the directory-separator, then all prior path-segments are discarded as they are considered "rooted". Difficulties then come from the fact relative-paths might include current or parent directory-names (like "./Temp/../../Temp2/temp.txt"). Combining a relative-path with an absolute-path might yield an unresolved path like "C:\Temp1\Temp2\..\..\Temp1.2\temp.txt". Some systems can handle these paths, some can't. But very likely a user will just want to see the combined result "C:\Temp1.2\temp.txt". So paths might need to be resolved. Finally, if path-parts are stringly-typed, methods overloads might not be able to express the required granularity to distinguish between operations. For example, how would the compiler know how to distinguish between these two methods (and how would you know?):

    string Combine(string directorySeparator, string pathSegment1, string pathSegment2);
    string Combine(params string[] pathSegments);
    
Both of these operations are valuable, so we cannot just remove one without limiting functionality. But in order to have both, one of the operations would have to have a different name. For example, the second operation might be called `CombineWithDefaultDirectorySeparator()`. But now the user must sort through all the different combine operations to select the right one, which is tiresome (and made more difficult by the fact they all take strings!) when all the user really wants to do is just simply combine some path-segments.

Far better is:

    string Combine(DirectorySeparator directorySeparator, PathSegment pathSegment1, PathSegment pathSegment2);
    string Combine(params PathSegment[] pathSegments);
    
Now it's perfectly clear which method to choose and in fact, the compiler will probably choose the right method just based on argument types.

### Resolve
Path resolution takes any relative-path directory-names (like '.' or '..') in the path and resolves them to the actual directory path.


   [TypedString]: <https://github.com/MinexAutomation/R5T.NetStandard.Types/wiki/TypedString>