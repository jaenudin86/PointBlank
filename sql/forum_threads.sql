-- phpMyAdmin SQL Dump
-- version 4.2.12deb2+deb8u2
-- http://www.phpmyadmin.net
--
-- Хост: localhost
-- Время создания: Окт 18 2016 г., 18:08
-- Версия сервера: 10.1.18-MariaDB-1~jessie
-- Версия PHP: 5.6.26-0+deb8u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `user64765_pb`
--

-- --------------------------------------------------------

--
-- Структура таблицы `forum_threads`
--

CREATE TABLE IF NOT EXISTS `forum_threads` (
`id` int(20) NOT NULL,
  `forumID` int(20) NOT NULL,
  `title` text NOT NULL,
  `post` text NOT NULL,
  `authorID` int(20) NOT NULL,
  `date` text NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `forum_threads`
--

INSERT INTO `forum_threads` (`id`, `forumID`, `title`, `post`, `authorID`, `date`) VALUES
(1, 1, 'Первый тред', 'Это первый тред на форуме', 1, '2:47 13.09.2016');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `forum_threads`
--
ALTER TABLE `forum_threads`
 ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `forum_threads`
--
ALTER TABLE `forum_threads`
MODIFY `id` int(20) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
