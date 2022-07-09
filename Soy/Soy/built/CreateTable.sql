CREATE TABLE T_ACCOUNT(
ACCOUNT_ID		INTEGER	PRIMARY KEY AUTOINCREMENT,
ACCOUNT_NAME		TEXT	NOT NULL UNIQUE,
ACCOUNT_DATETIME	DATE	NOT NULL DEFAULT CURRENT_DATE
);
CREATE TABLE T_SAVING(
SAVING_ID	INTEGER		PRIMARY KEY AUTOINCREMENT,
ACCOUNT_ID	INTEGER		NOT NULL,
SAVING_DATE	DATE		NOT NULL,
SAVING_AMOUNT	INTEGER		NOT NULL,
SAVING_DATETIME	DATETIME NOT NULL DEFAULT CURRENT_DATE,
FOREIGN KEY(ACCOUNT_ID) REFERENCES T_ACCOUNT(ACCOUNT_ID) ON DELETE CASCADE,
UNIQUE(SAVING_DATE,ACCOUNT_ID)
);
CREATE TABLE T_CARD(
CARD_ID		INTEGER	PRIMARY KEY AUTOINCREMENT,
CARD_NAME		TEXT	NOT NULL UNIQUE,
CARD_DATETIME	DATE	NOT NULL DEFAULT CURRENT_DATE
);
CREATE TABLE T_BILLING(
BILLING_ID	INTEGER		PRIMARY KEY AUTOINCREMENT,
CARD_ID	INTEGER		NOT NULL,
BILLING_DATE	DATE		NOT NULL,
BILLING_AMOUNT	INTEGER		NOT NULL,
BILLING_DATETIME	DATETIME NOT NULL DEFAULT CURRENT_DATE,
FOREIGN KEY(CARD_ID) REFERENCES T_CARD(CARD_ID) ON DELETE CASCADE,
UNIQUE(BILLING_DATE,CARD_ID)
);