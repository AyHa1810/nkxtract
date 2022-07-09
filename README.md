# nkxtract
Download: [![Build status](https://ci.appveyor.com/api/projects/status/5ldraia956ukr9jl?svg=true)](https://ci.appveyor.com/project/AyHa1810/nkxtract)

This is a tool to extract files from nkx archives (also known as monolith files).
These files are used to hold the samples for Kontakt instruments.

Currently the tool is hardcoded to look for keys in the registry for a single instrument only,
the RELEASE instrument. If you want to extract a different instrument's samples, you will
have to recompile this with the corresponding instrument name.

## Usage
```
nkxtract.exe c:\path\to\samples.nkx c:\output\directory\
```

## License
This is free software, made available under the terms of the GNU GPL, version 3.
See the included COPYING file for the full terms of the license.
