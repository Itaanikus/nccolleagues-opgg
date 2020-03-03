function myOpGgFunction() {
    var inputElement = document.getElementById("teamIdInput");
    var inputValue = inputElement.value
    var isNumber = Number.isInteger(inputValue);

    if (!isNumber || inputValue <= 0) {
        inputElement.focus();
        alert("Either the input wasn't a number, or it was less than or equal to 0. Please fix noob.");
        return;
    }

    opGgTeam(inputValue);
};

function opGgTeam(teamId) {
    var teamUrl = "https://app.esportligaen.dk/api/team/" + teamId + "?includeViewInfo=true";
    const teamResponse = fetchUrl(teamUrl);
};

function fetchUrl(url) {
    const response = fetch(url);
    return await response.json();
}