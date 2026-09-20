from src.models import COLUMNS

CELL_SEPARATOR = "\t|\t"
PERCENTILE_ROW_SEPARATOR = "\t\t|\t"


def show_regions_list(regions: set[str]) -> None:
    print("\n\nСписок всех регионов из списка для подсчета метрик:\n")
    for i, region in enumerate(sorted(regions), start=1):
        print(f"{i}. {region}")


def show_region_stats_table(header: list[str], region_stats_table: list[list[str]]) -> None:
    print("\nТаблица всех данных этого региона:\n")
    print(CELL_SEPARATOR.join(header))
    for row in region_stats_table:
        print(CELL_SEPARATOR.join(row))


def show_metrics() -> None:
    print("\nМетрики:")
    for i, column in enumerate(COLUMNS, start=1):
        print(f"{i} - {column.title}")


def show_results(
    min_: float,
    max_: float,
    mean: float,
    median: float,
    percentiles: dict[int, float],
) -> None:
    print(f"""
Минимум - {min_:.2f}
Максимум - {max_:.2f}
Среднее значение - {mean:.2f}
Медиана - {median:.2f}""")

    print("\nТаблица перцентилей:\n")
    print(f"перцентиль{CELL_SEPARATOR}значение")
    for percent, value in percentiles.items():
        print(f"{percent}%{PERCENTILE_ROW_SEPARATOR}{value:.2f}")
