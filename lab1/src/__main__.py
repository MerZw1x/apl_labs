import csv
import sys

import src.analytics.stats as s
import src.data_io.reader as r
import src.data_io.writer as w
from src.errors import DemographyError
from src.models import FILE_REQUIREMENTS, Statistics


def main() -> int:
    try:
        run()
    except (KeyboardInterrupt, EOFError):
        print("\nВыход")
    return 0

def load_data() -> list[list[str]]:
    data: list[list[str]] = []
    flag = True

    while flag:
        path = r.read_path()
        try:
            data = r.read_file(path)
            flag = False
        except DemographyError as e:
            print(f"Ошибка: {e}")
        except UnicodeDecodeError:
            print(f"Ошибка: файл не в кодировке utf-8. {FILE_REQUIREMENTS}")
        except csv.Error:
            print(f"Ошибка: файл не удалось разобрать. {FILE_REQUIREMENTS}")
        except OSError:
            print("Ошибка: не удалось открыть файл, проверь путь")

    return data
        
def run() -> None:
    data = load_data()
    regions = s.create_unique_region_set(data)
    header = s.get_header(data)

    flag = True
    while flag:
        try:
            flag = analyze(data, regions, header)
        except DemographyError as e:
            print(f"Попробуй другой регион или колонку: {e}")


def analyze(data: list[list[str]], regions: set[str], header: list[str]) -> bool:
    w.show_regions_list(regions)
    region_name = r.get_region_name(regions)
    region_stats_table = s.make_region_stats_table(data, region_name)
    w.show_region_stats_table(header, region_stats_table)
    w.show_metrics()
    column = s.select_column(r.get_stat_number())
    stats = s.get_necessary_region_stats(region_stats_table, column)
    w.show_results(Statistics(stats), s.calculate_percentiles(stats))
    return r.repeat()


if __name__ == "__main__":
    main()

# data/russian_demography.csv 
# data/big.csv               
# data/empty.csv              
# data/wrong_header.csv      
# data/not_csv.txt           
# data/bad_value.csv         
