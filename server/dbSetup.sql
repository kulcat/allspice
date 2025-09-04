CREATE TABLE IF NOT EXISTS Accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE Favorites (
    id INT NOT NULL PRIMARY KEY,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    recipe_id INT NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES Recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES Accounts (id) ON DELETE CASCADE,
    UNIQUE (recipe_id, account_id)
);

CREATE TABLE Recipes (
    id INT NOT NULL PRIMARY KEY,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    title VARCHAR(255) COMMENT 'Recipe Name',
    instructions VARCHAR(5000) COMMENT 'Recipe Instructions',
    img VARCHAR(1000) COMMENT 'Recipe Image',
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ) NOT NULL COMMENT 'Recipe Category',
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES Accounts (id) ON DELETE CASCADE
);

CREATE TABLE Ingredients (
    id INT NOT NULL PRIMARY KEY,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'Ingredient Name',
    quantity VARCHAR(255) COMMENT 'Ingredient Quantity',
    recipe_id INT NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES Recipes (id) ON DELETE CASCADE
);

DROP TABLE `Accounts`;

DROP TABLE `Favorites`;

DROP TABLE `Recipes`;

DROP TABLE `Ingredients`;