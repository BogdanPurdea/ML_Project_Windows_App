import pandas as pd

# =========================
# Configuration
# =========================
INPUT_FILE = "val_split_updated.csv"
OUTPUT_FILE = "val_split_updated_cleaned.csv"
SEPARATOR = ";"

NUMERIC_COLUMNS = [
    "energy_kcal",
    "sugar_g",
    "sat_fat_g",
    "salt_g",
    "fiber_g",
    "protein_g",
    "carbohydrate_g",
    "total_fat_g"
]

# Domain constraints
MAX_CONSTRAINTS = {
    "energy_kcal": 990,
    "sugar_g": 100,
    "sat_fat_g": 100,
    "salt_g": 100,
    "fiber_g": 100,
    "protein_g": 100,
    "carbohydrate_g": 100,
    "total_fat_g": 100,
}

MIN_VALUE = 0

# =========================
# Load dataset
# =========================
df = pd.read_csv(INPUT_FILE, sep=SEPARATOR)

initial_count = len(df)

# =========================
# Remove wrong / invalid data
# =========================
valid_mask = pd.Series(True, index=df.index)

# Minimum value checks and NaN removal
for col in NUMERIC_COLUMNS:
    valid_mask &= df[col].notna()
    valid_mask &= df[col] >= MIN_VALUE

# Maximum value checks
for col, max_val in MAX_CONSTRAINTS.items():
    valid_mask &= df[col] <= max_val

df_clean = df[valid_mask].copy()

removed_count = initial_count - len(df_clean)

# =========================
# Save cleaned dataset
# =========================
df_clean.to_csv(
    OUTPUT_FILE,
    sep=SEPARATOR,
    index=False
)

# =========================
# Reporting
# =========================
print("Data cleaning completed successfully")
print(f"Initial records: {initial_count}")
print(f"Removed invalid records: {removed_count}")
print(f"Remaining records: {len(df_clean)}")
print(f"Output file: {OUTPUT_FILE}")
