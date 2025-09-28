# ADVANCED-BUSINESS-DEVELOPMENT-WITH-.NET

DROP USER IF EXISTS 'mottuser'@'%';
CREATE USER 'mottuser'@'%' IDENTIFIED WITH mysql_native_password BY 'mottupass';
GRANT CREATE, SELECT, INSERT, UPDATE, DELETE ON mottuDB.* TO 'mottuser'@'%';
FLUSH PRIVILEGES;

DROP USER IF EXISTS 'mottuser'@'mydb-challenge-mottu.mysql.database.azure.com';
CREATE USER 'mottuser'@'mydb-challenge-mottu.mysql.database.azure.com'
  IDENTIFIED WITH mysql_native_password BY 'mottupass';

GRANT CREATE, SELECT, INSERT, UPDATE, DELETE
  ON mottuDB.* TO 'mottuser'@'mydb-challenge-mottu.mysql.database.azure.com';

FLUSH PRIVILEGES;
