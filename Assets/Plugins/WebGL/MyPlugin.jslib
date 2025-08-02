mergeInto(LibraryManager.library, {
  ShowAlert: function () {
    alert("NullReferenceException: Object reference not set to an instance of an object\n" +
                   "GameManager.Update () (at Assets/Scripts/GameManager.cs:42)");
  }
});
