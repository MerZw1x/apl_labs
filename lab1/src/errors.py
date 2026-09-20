class DemographyError(Exception):
    """Базовая ошибка приложения"""


class FileFormatError(DemographyError):
    """Ошибки формата файла"""


class NoDataError(DemographyError):
    """Нет значений"""
