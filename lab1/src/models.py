from typing import NamedTuple


class Column(NamedTuple):
    key: str
    title: str
    index: int


HEADER_ROW_INDEX = 0
REGION_INDEX_IN_ROW = 1

EXPECTED_HEADER = [
    "year",
    "region",
    "npg",
    "birth_rate",
    "death_rate",
    "gdw",
    "urbanization",
]

COLUMNS = (
    Column("npg", "natural population growth", 2),
    Column("birth_rate", "birth rate", 3),
    Column("death_rate", "death rate", 4),
    Column("gdw", "gdw", 5),
    Column("urbanization", "urbanization", 6),
)
