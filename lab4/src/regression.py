import pandas as pd
import numpy as np
from sklearn.model_selection import GroupShuffleSplit
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_absolute_error, r2_score

def train_model(data: pd.DataFrame) -> LinearRegression:
    X = data[["last_hits"]]
    y = data["gold_per_min"]

    splitter = GroupShuffleSplit(n_splits=1, test_size=0.2, random_state=42)

    train_indices, test_indices = next(splitter.split(X, y, groups=data["match_id"]))

    X_train = X.iloc[train_indices]
    X_test  = X.iloc[test_indices]
    y_train = y.iloc[train_indices]
    y_test  = y.iloc[test_indices]

    model = LinearRegression()
    model.fit(X_train, y_train)

    y_pred = model.predict(X_test)
    mae = mean_absolute_error(y_test, y_pred)
    r2 = r2_score(y_test, y_pred)

    return model

def predict_gpm(model: LinearRegression, krip_stat: int) -> float:
    df = pd.DataFrame({"last_hits": [krip_stat]})
    res = model.predict(df)
    return res[0]

def get_line_points(model: LinearRegression, data:pd.DataFrame) -> tuple[pd.DataFrame, np.ndarray]:
    x_line = pd.DataFrame({"last_hits": [data["last_hits"].min(), data["last_hits"].max()]})
    y_line = model.predict(x_line)
    return x_line, y_line