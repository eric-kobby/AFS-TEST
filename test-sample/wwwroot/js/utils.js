var utilHelper = (function(){

  function UtilHelper() {}

  UtilHelper.prototype.toggleDisable = function(id) {
    const el = document.querySelector(id);
    const disabled = el.getAttribute('disabled');
    if (disabled) return el.removeAttribute("disabled");
    el.setAttribute("disabled", true);
  }
  return new UtilHelper();

})();