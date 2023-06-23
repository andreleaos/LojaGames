USE master

IF DB_ID('lojaGamesDB') IS NULL
BEGIN
    CREATE DATABASE lojaGamesDB;
END