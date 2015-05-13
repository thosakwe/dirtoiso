# dirtoiso
Generates an ISO 9660 file at the Windows command line from any existing directory.

#Why?
I want to write an operating system. However, I am lazy to repeatedly run multiple scripts and commands just to build an x86 system on Windows. Also, there is no command line-based program that builds an .iso out of the box. Until now.

#Usage

Syntax: dirtoiso [options]
Options:
	*  -d: Specifies input directory path.
	*  -o: Specifies output filename.
	*  -id: Specifies an output volume identifier.
	*  -h: Shows a help view.

If no arguments are supplied, DirToIso will create an iso file from the current directory.

#Installation
You do not have to build anything. The current version is located in /bin. The DLL's present in that folder are required for it to run. Shout out to [.NET DiscUtils](http://discutils.codeplex.com).

#Note
Thank you for using this! Feel free to modify it or report issues as you please.
