import src.data_io as io
import src.regression as r
import src.visualisation as v

from src.errors import (
    FileFormatError
)

from src.models import (
    FILE_REQUIREMENTS
)

FILE_PATH = "data/players.csv"
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
        try:
            data = io.read_file(path)
            flag = False
        except FileFormatError as e:
            print(f"Ошибка: {e}")
        except UnicodeDecodeError:
            print(f"Ошибка: файл не в кодировке utf-8. {FILE_REQUIREMENTS}")
        except OSError:
            print("Ошибка: не удалось открыть файл, проверь путь")


def run():
    data = io.read_file(FILE_PATH)
    model = r.train_model(data)
    x_line, y_line = r.get_line_points(model, data)

    flag = True
    while flag:
        krip_stat = io.get_krip_stat()

        predict = r.predict_gpm(model, krip_stat)
        io.show_predict(predict)

        v.show_plot(data, x_line, y_line, krip_stat, predict)
        flag = io.repeat()
        
if __name__ == "__main__":
    main()
