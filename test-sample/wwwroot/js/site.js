'use strict';
$(function() {
  const logoutButton = $('#logout-button');

  logoutButton?.on('click', () => {
    $.post('/api/auth/logout', () => location.href = "/auth/login");
  });

});

