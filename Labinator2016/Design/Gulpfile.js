/*
Gulpfile.js file for the tutorial:
Using Gulp, SASS and Browser-Sync for your front end web development - DESIGNfromWITHIN
http://designfromwithin.com/blog/gulp-sass-browser-sync-front-end-dev

Steps:

1. Install gulp globally:
npm install --global gulp

2. Type the following after navigating in your project folder:
npm install gulp gulp-util gulp-sass gulp-uglify gulp-rename gulp-minify-css gulp-notify gulp-concat gulp-plumber browser-sync --save-dev

3. Move this file in your project folder

4. Setup your vhosts or just use static server (see 'Prepare Browser-sync for localhost' below)

5. Type 'Gulp' and ster developing
*/

/* Needed gulp config */
var gulp = require('gulp');  
var sass = require('gulp-sass');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
var notify = require('gulp-notify');
var minifycss = require('gulp-minify-css');
var concat = require('gulp-concat');
var plumber = require('gulp-plumber');
var browserSync = require('browser-sync');
var url = require('url'); // https://www.npmjs.org/package/url
var reload = browserSync.reload;

var paths = {
	scss: './sass/*.scss'
};
gulp.task('sass',function(){
	gulp.src('scss/app.scss')
		.pipe(sass({
			includePaths:['scss']
		}))
		.pipe(gulp.dest('../Content'));
});
gulp.task('browser-sync', function () {
    browserSync.init(["../Content/*.css","../Scripts/*.js","*.html"],{
        server: {
            baseDir: "./",
            routes:{
                "/Content": "../Content",
                "/Images": "../Images",
                "/Scripts": "../Scripts"
            }
        }
    })});

gulp.task('watch',['sass','browser-sync'],function(){
	gulp.watch(["scss/*.scss","scss/base/*.scss","scss/sections/*.scss","scss/style/*.scss"],['sass']);
});
