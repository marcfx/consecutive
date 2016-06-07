# consecutive

Console application that processes large files (tested on 20GB) with  UInt32 numers separated by space. 
It provides list of consecutive number groups to the console output or into the file.

## Usage
Consecutive.Console.exe -i InputFileName.txt -o OutputFileName.txt -a (algorithm:BitMask|InMemorySimple|ExternalMergeSort)

### Generate sample file
Generate sample file
Consecutive.Console.exe -s Sample.txt -x 1000000 -i Sample.txt -o Output.txt

## Algorithms
### InMemorySimple
Very fast, but requires lot of memory. For 1.0000.000 (100MB input file) requires 1.5GB of RAM.
*Do not use on big files!*

### BitMask
Always take some time (2 minutes), but requires at most 500MB of RAM.
- Test: 10.7GB took 17 minutes.

### External Merge Sort
Very low memory consumption (up to 100MB), but it requires heavy disk activity. Uses [external merge sort](https://en.wikipedia.org/wiki/External_sorting) algorithm.

- Test: 10.7GB took 50 minutes.


