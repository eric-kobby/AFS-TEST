"use strict";
$(function () {

  $('#register-form').parsley();

  function toggleElements() {
    $('#spinner').toggleClass('d-none');
    utilHelper.toggleDisable('#register-button');
  }
  
  $("#register-form").on("submit", (e) => {
    e.preventDefault();
    toggleElements();
    $.post("/api/auth/register", $(e.target).serialize(), () => location.href = "/Auth/Login")
    .fail(error => {
      toggleElements();
      if (error.status === 401) return alert(error.responseText);
    });
  });
});
