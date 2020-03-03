const leagueGameTypeId = 2;

function myOpGgFunction() {
    var inputElement = document.getElementById("teamIdInput");
    var inputValue = +inputElement.value
    var isNumber = Number.isInteger(inputValue);

    if (!isNumber || inputValue <= 0) {
        inputElement.focus();
        alert("Either the input wasn't a number, or it was less than or equal to 0. Please fix noob.");
        return;
    }

    getTeamInfo(inputValue).then(teamMembers => {
        console.log(teamMembers)
        teamMembers.forEach(member => {
            console.log(member);
        });
    });
}

function getTeamInfo(teamId) {
    const teamUrl = "https://app.esportligaen.dk/api/team/" + teamId + "?includeViewInfo=true";

    return fetchUrl(teamUrl).then(response => {
        var teamMembers = [];        
        response.members.forEach(member => {
            var nickName = member.user.nickname;
            
            const userUrl = "https://app.esportligaen.dk/api/user/" + member.user.id + "?includeGameTeamInfo=true"
            fetchUrl(userUrl).then(response => {
                var gamerIds = [];
                response.gameLogins.filter(login => login.gameLoginTypeId === leagueGameTypeId).forEach(login => {
                    gamerIds.push(login.gamerId)
                });

                gamerIds.length > 0 ? 
                Array.prototype.push.apply(teamMembers, gamerIds)
                : teamMembers.push(nickName);
            });            
        });

        return teamMembers;
    });
}

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