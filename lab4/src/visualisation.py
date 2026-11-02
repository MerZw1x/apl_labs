import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

def show_plot(data: pd.DataFrame, x_line: pd.DataFrame, y_line: np.ndarray, krip_stat: int, predict: float):
    fig, ax = plt.subplots(figsize=(10, 6))
    ax.scatter(
        data["last_hits"], data["gold_per_min"],
        s=2, alpha=0.1, label="Результаты игроков"
    )

    ax.plot(
        x_line["last_hits"], y_line,
        color="orange", label="Линейная регрессия"
    )

    ax.scatter(
        krip_stat, predict,
        color="red", s=100, zorder=3, label="Прогноз"
    )

    ax.annotate(
        f"{predict:.2f} GPM",
        xy=(krip_stat, predict),
        xytext=(10, 10),
        textcoords="offset points",
        color="red",
    )

    ax.hlines(
        y=predict, xmin=0, xmax=krip_stat,
        colors="red", linestyles="--", alpha=0.5
    )

    ax.legend()

    ax.set_title("Зависимость GPM от количества добитых крипов")
    ax.set_xlabel("Количество добитых крипов, шт")
    ax.set_ylabel("Золото в минуту, GPM")
    ax.grid(True)

    ax.set_xlim(left=0)
    ax.set_ylim(bottom=0)
    plt.show()
