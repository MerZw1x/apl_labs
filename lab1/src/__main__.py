import sys

import src.data_io.reader as reader

def main() :
    path = reader.read_path()
    reader.read_file(path)

if __name__ == "__main__":
    sys.exit(main())

# /Users/tim/Desktop/APL_LABS/lab1/data/russian_demography.csv