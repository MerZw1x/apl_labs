from src.errors import FileFormatError, NoDataError
from src.models import COLUMNS, HEADER_ROW_INDEX, REGION_INDEX_IN_ROW, Column

PERCENTILE_STEP = 5


def get_header(data: list[list[str]]) -> list[str]:
    return data[HEADER_ROW_INDEX]


def create_unique_region_set(data: list[list[str]]) -> set[str]:
    res: set[str] = set()

    for row in data[HEADER_ROW_INDEX + 1 :]:
        res.add(row[REGION_INDEX_IN_ROW])

    return res


def select_column(number: int) -> Column:
    return COLUMNS[number - 1]


def make_region_stats_table(data: list[list[str]], region_name: str) -> list[list[str]]:
    res: list[list[str]] = []

    for row in data[HEADER_ROW_INDEX + 1 :]:
        if row[REGION_INDEX_IN_ROW] == region_name:
            res.append(row)

    return res


def get_necessary_region_stats(region_stats_table: list[list[str]], column: Column) -> list[float]:
    if not region_stats_table:
        raise NoDataError("Статистика по региону пустая")

    stats: list[float] = []

    for row in region_stats_table:
        try:
            stats.append(float(row[column.index]))
        except ValueError as e:
            raise FileFormatError(f"Некорректная строка данных: {row}") from e

    return stats


def calculate_percentiles(stats: list[float]) -> dict[int, float]:
    ordered = sorted(stats)
    n = len(ordered)
    res: dict[int, float] = {}

    for percent in range(0, 101, PERCENTILE_STEP):
        rank = -(-percent * n // 100)
        res[percent] = ordered[max(rank, 1) - 1]

    return res
