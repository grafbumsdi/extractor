$path_to_dll = "./src/Extractor/bin/Debug/netcoreapp2.0/Extractor.dll"

dotnet $path_to_dll wikifolio D65C86B7-C217-42C8-9E9B-00514064145F --withFees --withRecentVirtualOrderGroups 100 --withItems --outputfile "demooutput.txt"

pause