
$(document).ready(() => {
  const alert = $('#alert');
  const message = alert.attr('data-message');

  if (message) {
    $('#alertText').text(message);
    alert.addClass('show');

    setTimeout(function() {
      alert.alert('close');
    }, 3000);
  }
})
