# Hair Salon
## by Keely Silva-Glenn

## Description

This app is for venues and bands. The app allows users to add bands and venues, connect the two, and see the correlation. The user can see the bands for each venue and the venues for a band.

## Setup and Installation

* Clone repository
* Open index.html in browser of your choice

### Recreate Database

In SQLCMD:
* CREATE DATABASE band_tracker;
* GO
* USE band_tracker;
* GO
* CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));
* GO
* CREATE TABLE stylist (id INT IDENTITY(1,1)m name VARCHAR(255));
* GO

## Link
https://github.com/keelyzglenn/Concert-Week-4-Project

## Technologies Used

* HTML
* Bootstrap
* CSS
* C#
* Nancy
* SQL

## Specs
#### The user can Create. Read(Show All and Show One). Update. Delete Venues
* Input: Add Venue, See Venue, See All Venues, Update Venue, Delete Venue
* Output: Venue Added. Returns List of All Venues, Returns individual Venue. Venue Updated!. Venue Deleted

#### User can create Bands that have played at a Venue.
* Input: Add Band to Venue
* Output: Success

#### User cannot update or delete bands
* Input: Update Band
* Output: Sorry that is not available

#### On individual venue page the user can see a list of all bands that have played there and they can add bands to that venue.
* Input: See All Venues Bands
* Output: Returns list of Bands for that venue

#### On individual van project the user can see a list of all venues the band has played at and they can add a venue to that band.
* Input: See all band Venues
* Output: Returns list of venues for that band

## Legal
MIT License

Copyright (c) 2017 Keely Silva-Glenn
