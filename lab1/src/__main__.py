import csv
import sys

import src.analytics.stats as s
import src.data_io.reader as r
import src.data_io.writer as w
from src.errors import DemographyError


def main() -> int:
    try:
        run()
    except (KeyboardInterrupt, EOFError):
        print("\nВыход")
        return 0
    except DemographyError as e:
        print(f"Ошибка: {e}")
        return 1
    except UnicodeDecodeError:
        print("Это не текстовый файл")
        return 1
    except csv.Error as e:
        print(f"Файл не удалось разобрать как CSV: {e}")
        return 1
    except OSError as e:
        print(f"Не удалось открыть файл: {e.strerror}")
        return 1
    return 0


def run() -> None:
    path = r.read_path()
    data = r.read_file(path)
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
    w.show_results(
        s.calculate_min(stats),
        s.calculate_max(stats),
        s.calculate_mean(stats),
        s.calculate_median(stats),
        s.calculate_percentiles(stats),
    )
    return r.repeat()


if __name__ == "__main__":
    sys.exit(main())

# /Users/tim/Desktop/APL_LABS/lab1/data/russian_demography.csv
