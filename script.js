function myOpGgFunction() {
    var inputElement = document.getElementById("teamIdInput");
    var inputValue = +inputElement.value
    var isNumber = Number.isInteger(inputValue);

    if (!isNumber || inputValue <= 0) {
        inputElement.focus();
        alert("Either the input wasn't a number, or it was less than or equal to 0. Please fix noob.");
        return;
    }

    getTeamInfo(inputValue);
};

function getTeamInfo(teamId) {
    var teamUrl = "https://app.esportligaen.dk/api/team/" + teamId + "?includeViewInfo=true";

    return fetchUrl(teamUrl).then(response => {
        // Members from json object..
        var teamMembers = [];        
        response.members.forEach(member => {
            teamMembers.push( { id: member.id, nickname: member.nickname } )
        });
        
    });
};

function fetchUrl(url) {
    console.log("Fetching url: " + url);

    return fetch(url).then(response => {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response.json();
    }).then(data => {
        return data;
    }).catch(function (error) {
        console.log(error);
    })
}