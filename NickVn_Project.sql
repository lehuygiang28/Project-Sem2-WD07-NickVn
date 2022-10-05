CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `categories` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `title` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `action` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `img_sale_off` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `img_src` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `total` int(11) NOT NULL,
    `note` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `status` int(11) NOT NULL DEFAULT '1',
    CONSTRAINT `PK_categories` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `googlerecaptcha` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `host_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `site_key` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `secret_key` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `Response` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    CONSTRAINT `PK_googlerecaptcha` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `images` (
    `img_id` int(11) NOT NULL AUTO_INCREMENT,
    `lienminh_id` int(11) NOT NULL,
    `img_link` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    CONSTRAINT `PRIMARY` PRIMARY KEY (`img_id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `lienminh` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `product_user_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `product_user_password` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `publisher` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `price_atm` decimal(10) NOT NULL,
    `champ` int(11) NOT NULL,
    `skin` int(11) NOT NULL,
    `rank` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `status` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `note` int(11) NULL,
    `img_thumb` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `img_src` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `sold` int(11) NOT NULL,
    CONSTRAINT `PK_lienminh` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `oders` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `oder_id` int(11) NOT NULL,
    `user_id` int(11) NOT NULL,
    `product_id` int(11) NOT NULL,
    `create_at` datetime NOT NULL,
    `update_at` datetime NOT NULL,
    CONSTRAINT `PK_oders` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `product_category` (
    `index_no` int(11) NOT NULL AUTO_INCREMENT,
    `category_id` int(11) NOT NULL,
    `category_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `action` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `img_src` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `total` int(11) NOT NULL,
    `note` int(11) NULL,
    `status` int(11) NOT NULL DEFAULT '1',
    CONSTRAINT `PRIMARY` PRIMARY KEY (`index_no`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `roles` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `role_id` int(11) NOT NULL DEFAULT '1',
    `role_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `role_name_en` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    CONSTRAINT `PK_roles` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `users` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `user_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `password` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `first_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `last_name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `phone` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `email` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `money` decimal(10) NOT NULL,
    `role_id` int(11) NOT NULL DEFAULT '1',
    `img_src` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `cover_img_src` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `note` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `create_at` datetime NULL,
    `update_at` datetime NULL,
    `last_login` datetime NULL,
    CONSTRAINT `PK_users` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221005051456_Init_Migration', '6.0.5');

COMMIT;

