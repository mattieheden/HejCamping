// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// JavaScript code
document.addEventListener("DOMContentLoaded", function() {
  // Get the modal
  var modal = document.getElementById("modal");

  // Delay in milliseconds (e.g., 3000ms = 3 seconds)
  var delay1 = 3000;
  
  var delay2 = 7000

  // Show the modal after the delay
  setTimeout(function() {
      modal.style.display = "block";
  }, delay1);

  //setTimeout(function() {
  //    modal.style.display = "none";
  //}, delay2);
});
