-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 07, 2022 at 08:17 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `nickvn_project`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `title` text NOT NULL,
  `action` text NOT NULL,
  `img_sale_off` text NOT NULL,
  `img_src` text NOT NULL,
  `total` int(11) NOT NULL DEFAULT 0,
  `note` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`id`, `name`, `title`, `action`, `img_sale_off`, `img_src`, `total`, `note`) VALUES
(1, 'Lien Minh', 'Danh mục game liên minh', 'Garena', '/storage/images/sale-off-30.png', '/storage/images/lien-minh.gif', 12335, ''),
(2, 'Liên Quân', 'Danh mục game Liên Quân', 'Garena', '/storage/images/sale-off-30.png', '/storage/images/lien-quan.gif', 24235, '');

-- --------------------------------------------------------

--
-- Table structure for table `googlerecaptcha`
--

CREATE TABLE `googlerecaptcha` (
  `id` int(11) NOT NULL,
  `host_name` text DEFAULT NULL,
  `site_key` text NOT NULL,
  `secret_key` text NOT NULL,
  `Response` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `googlerecaptcha`
--

INSERT INTO `googlerecaptcha` (`id`, `host_name`, `site_key`, `secret_key`, `Response`) VALUES
(1, 'localhost', '6LdZFd4hAAAAAH3CzSQKeHMOd-N9taof0XE7bpUd', '6LdZFd4hAAAAAOcaKQEVn-EP4AuyLL-wLqnykBXa', NULL),
(2, '168.138.179.121', '6Lc55R4fAAAAAEh4qaPNR6fVcwu44zSkIHKrVMng', '6Lc55R4fAAAAALGca6ts3hikUsFbjHT74ytvHqoR', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

CREATE TABLE `images` (
  `img_id` int(11) NOT NULL,
  `lienminh_id` int(11) NOT NULL,
  `img_link` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `images`
--

INSERT INTO `images` (`img_id`, `lienminh_id`, `img_link`) VALUES
(1, 4453, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(2, 4453, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(3, 4453, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(4, 4453, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(5, 4453, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(6, 4453, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(7, 4454, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(8, 4454, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(9, 4454, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(10, 4454, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(11, 4454, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(12, 4454, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(13, 4455, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(14, 4455, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(15, 4455, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(16, 4455, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(17, 4455, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(18, 4455, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(19, 4456, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(20, 4456, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(21, 4456, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(22, 4456, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(23, 4456, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(24, 4456, '/storage/images/xxuC88f0h9_1632531414.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `lienminh`
--

CREATE TABLE `lienminh` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `price_atm` decimal(10,0) NOT NULL DEFAULT 0,
  `champ` int(11) NOT NULL DEFAULT 0,
  `skin` int(11) NOT NULL DEFAULT 0,
  `rank` text NOT NULL,
  `status` text NOT NULL,
  `note` int(11) DEFAULT NULL,
  `img_thumb` text NOT NULL,
  `img_src` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `lienminh`
--

INSERT INTO `lienminh` (`id`, `name`, `price_atm`, `champ`, `skin`, `rank`, `status`, `note`, `img_thumb`, `img_src`) VALUES
(4453, 'Liên Minh', '204000', 120, 33, 'Chưa Rank', 'Trắng Thông Tin', 0, '/storage/images/FSPfB05HiR_1632531414.jpg', '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(4454, 'Liên Minh', '56000', 12, 33, 'Chưa Rank', 'Trắng Thông Tin', 0, '/storage/images/FSPfB05HiR_1632531414.jpg', ''),
(45557, 'Liên Minh', '333221', 99, 100, 'Chưa Rank', 'Trắng Thông Tin', 0, '/storage/images/FSPfB05HiR_1632531414.jpg', ''),
(45558, 'Liên Minh', '56000', 44, 15, 'Chưa Rank', 'Trắng Thông Tin', 0, '/storage/images/FSPfB05HiR_1632531414.jpg', ''),
(45559, 'Liên Minh', '500000', 123, 123, 'Chưa Rank', 'Trắng Thông Tin', 0, '/storage/images/FSPfB05HiR_1632531414.jpg', ''),
(45560, 'Liên Minh', '400001', 80, 70, 'Chưa Rank', 'Trắng Thông Tin', 0, '/storage/images/FSPfB05HiR_1632531414.jpg', '');

-- --------------------------------------------------------

--
-- Table structure for table `product_category`
--

CREATE TABLE `product_category` (
  `category_id` int(11) NOT NULL DEFAULT 0,
  `category_name` text NOT NULL,
  `action` text DEFAULT NULL,
  `img_src` text NOT NULL,
  `total` int(11) NOT NULL DEFAULT 0,
  `note` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `product_category`
--

INSERT INTO `product_category` (`category_id`, `category_name`, `action`, `img_src`, `total`, `note`) VALUES
(1, 'Liên Minh', 'LienMinh', '/storage/images/lien-minh.gif', 14441, 0),
(1, 'Thử Vận May Liên Minh', 'ThuVanMay', '/storage/images/thu-van-may.gif', 442, 1);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `user_name` text NOT NULL,
  `password` text NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `phone` text DEFAULT NULL,
  `email` text NOT NULL,
  `money` float NOT NULL DEFAULT 0,
  `role` int(11) NOT NULL DEFAULT 1,
  `note` text DEFAULT NULL,
  `create_at` datetime DEFAULT NULL,
  `update_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `user_name`, `password`, `first_name`, `last_name`, `phone`, `email`, `money`, `role`, `note`, `create_at`, `update_at`) VALUES
(1, 'giang', '0c40ca0420380a00b902308200d0cc05009a06f07508409b', 'giang', 'huy', '0981333332', 'giang@vtc.edu.vn', 145000, 1, 'pw: 1', '0000-00-00 00:00:00', '0000-00-00 00:00:00'),
(2, 'giangadmin', '0ff0440e101c0b80140ee02509b0f80ff0e20e502f01301f', 'NickVn', '9/7/2022 19:24:54', '0123456789', '28122002g@gmail.com', 0, 1, NULL, NULL, NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `googlerecaptcha`
--
ALTER TABLE `googlerecaptcha`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `images`
--
ALTER TABLE `images`
  ADD PRIMARY KEY (`img_id`);

--
-- Indexes for table `lienminh`
--
ALTER TABLE `lienminh`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `googlerecaptcha`
--
ALTER TABLE `googlerecaptcha`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `images`
--
ALTER TABLE `images`
  MODIFY `img_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `lienminh`
--
ALTER TABLE `lienminh`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45561;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
