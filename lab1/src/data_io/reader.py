def read_path() -> str:
    path = input("Привет, чтобы узнать метрики, отправь сюда путь до файла:\n")
    return path

def read_file(path: str):
    f = open(path, "r")
    print(type(f))