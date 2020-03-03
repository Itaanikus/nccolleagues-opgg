function myOpGgFunction() {
    var inputElement = document.getElementById("teamIdInput");
    var inputValue = inputElement.value
    var isNumber = Number.isInteger(inputValue);

    if (!isNumber || inputValue <= 0) {
        inputElement.focus();
        alert("Either the input wasn't a number, or it was less than or equal to 0. Please fix noob.");
        return;
    }

    var tmp = getTeamInfo(inputValue);
    console.log(getTeamInfo)
};

function getTeamInfo(teamId) {
    var teamUrl = "https://app.esportligaen.dk/api/team/" + teamId + "?includeViewInfo=true";
    const teamResponse = fetchUrl(teamUrl);

    // Members from json object..
    var teamMembers = [];
    teamResponse.members.forEach(member => {
        teamMembers.push( { id: member.id, nickname: member.nickname } )
    });
    return teamMembers;
};

function fetchUrl(url) {
    fetch(url).then(function (response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }

        response.json().then(function (data) {
            return data;
        })
    }).catch(function (error) {
        console.log(error);
    })
}