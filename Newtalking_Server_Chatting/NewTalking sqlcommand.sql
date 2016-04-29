CREATE DATABASE NewTalking;

CREATE TABLE users (user_id INTEGER PRIMARY KEY AUTO_INCREMENT, user_name VARCHAR(30) NOT NULL, user_password VARCHAR(20) NOT NULL);

CREATE TABLE users_information (user_id INTEGER, user_sex CHAR(2), user_birthday DATE, user_phone INTEGER, FOREIGN KEY(user_id) REFERENCES users(user_id));

CREATE TABLE users_online(user_id INTEGER);

CREATE TABLE over_messages(sender_id INTEGER, receiver_id INTEGER, time DATETIME, message VARCHAR(1438));

CREATE TABLE following_list(user_id INTEGER, follow_id INTEGER);
CREATE TABLE black_list(user_id INTEGER, black_id INTEGER);


--INSERT INTO users(user_name, user_password) VALUES ('大明', 123456);

--INSERT INTO users(user_name, user_password) VALUES ('小明', 123456);

--INSERT INTO users_information VALUES (1, '男', 946659661, 10086);

--INSERT INTO users_information VALUES (2, '男', 946659662, 10010);


--SELECT * FROM users INNER JOIN users_information ON users.user_id = users_information.user_id;

--on user.user_id like 'w%';,,,~~



--插入用户
--INSERT INTO users(user_name, user_password) VALUES (?, ?);


--搜索最大值
--SELECT LAST_INSERT_ID();


--插入用户资料
--INSERT INTO users_information VALUES (?, ?, ?, ?);


--修改全部信息
--UPDATE users_information SET user_sex = ?, user_birthday = ?, user_phone = ? WHERE user_id = ?;


--修改密码
--UPDATE users SET user_password = ？ WHERE user_id = ?;


--查找密码
--SELECT user_password FROM users WHERE user_id = ?;


--查找全部信息
--SELECT * FROM users_information WHERE user_id = ?;


--查找用户名
--SELECT user_name FROM users WHERE user_id = ?;