'use strict';
$(function() {

  function toggleElements() {
    $('#spinner').toggleClass('d-none');
    utilHelper.toggleDisable('#login-button');
  }

  $('#login-form').on('submit', e => {
    e.preventDefault();
    toggleElements();
    $.post('/api/auth/login', $(e.target).serialize(), () => {
      location.href = "/"
    }).fail(error => {
      toggleElements();
      if (error.status === 401) return alert(error.responseText);
    });
  });

});