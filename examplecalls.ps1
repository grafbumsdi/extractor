$path_to_dll = "./src/Extractor/bin/Debug/netcoreapp2.0/Extractor.dll"

dotnet $path_to_dll wikifolio 742CFBA8-B00A-433B-B468-01A00FDFEB0A --withFees --withRecentVirtualOrderGroups 100 --withItems --outputfile "demooutput.txt"

pause