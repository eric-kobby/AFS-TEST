$(function () {
  const spinner = $("#spinner");

  const toggleElements = () => {
    spinner.toggleClass("d-none");
    utilHelper.toggleDisable('#translate-button');
  };

  $("#translation-form").on("submit", async (e) => {
    e.preventDefault();
    toggleElements();
    $.post("/api/translations", $(e.target).serialize(), () =>
      setTimeout(() => (location.href = "/Home/Translations"), 300)
    ).fail(toggleElements);
  });
});
