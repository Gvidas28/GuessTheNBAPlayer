import React, { useState, useEffect } from "react";
import "./custom.css";

export default function App() {
  const [teams, setTeams] = useState(null);
  const [player, setPlayer] = useState(null);
  const [points, setPoints] = useState(null);
  const [difficulty, setDifficulty] = useState("1");

  const renderPlayerData = () => {
    return (
      <div>
        <p>Name: {player.data.name}</p>
        <p>Position: {player.data.position}</p>
        <p>Height: {player.data.height}</p>
        <p>Weight: {player.data.weight}</p>
      </div>
    );
  };

  const handleDropdownChange = (event) => {
    setDifficulty(event.target.value);
  };

  const guess = (teamId) => {
    const scoreRequest = {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        CurrentScore: points ? points.data.updatedScore : 0,
        PlayerID: player.data.id,
        TeamID: teamId,
        Difficulty: parseInt(difficulty, 10)
      })
    };

    console.log(scoreRequest.body);

    fetch("score/updatescore", scoreRequest)
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        if (data && data.success) {
          setPoints(data);
        }
        let pointsInfo = data && data.success ? data : points.data.updatedScore;
        fetch("nbadata/getrandomplayer")
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            setPlayer(data);
            let playerInfo = data;
            const teamsRequest = {
              method: "POST",
              headers: {
                "Content-Type": "application/json"
              },
              body: JSON.stringify({
                CurrentScore: pointsInfo ? pointsInfo.data.updatedScore : 0,
                Difficulty: parseInt(difficulty, 10),
                PlayerID: playerInfo.data.id
              })
            };

            fetch("nbadata/getteams", teamsRequest)
              .then((response) => response.json())
              .then((data) => {
                console.log(data);
                setTeams(data);
              })
              .catch((error) => console.log(error));
          })
          .catch((error) => console.log(error));
      })
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    fetch("nbadata/getrandomplayer")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setPlayer(data);
        let playerInfo = data;

        const request = {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({
            CurrentScore: points ? points.data.updatedScore : 0,
            Difficulty: parseInt(difficulty, 10),
            PlayerID: playerInfo.data.id
          })
        };

        fetch("nbadata/getteams", request)
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            setTeams(data);
          })
          .catch((error) => console.log(error));
      })
      .catch((error) => console.log(error));
  }, []);

  return (
    <div className="background">
      <div className="points">
        Points: {points ? points.data.updatedScore : 0}
      </div>
      <div className="dropdownas">
        <h1>Difficulty:</h1>
        <select
          className="pats-dropdownas"
          value={difficulty}
          onChange={handleDropdownChange}
        >
          <option value="1">Easy</option>
          <option value="2">Normal</option>
          <option value="3">Hard</option>
        </select>
      </div>
      <div className="actual-team">
        {points && points.success
          ? points.data.correct
            ? "Correct!"
            : `Incorrect! Player's actual team is ${points.data.actualTeam}`
          : points
          ? points.errorMessage
          : null}
      </div>
      <div className="team-list">
        <h1>Teams:</h1>
        {teams && teams.success
          ? teams.data.map((list, index) => (
              <button
                onClick={() => guess(list.id)}
                className="team-button"
                key={index}
              >
                {list.name}
              </button>
            ))
          : teams
          ? teams.errorMessage
          : null}
      </div>
      <div className="player-info-box">
        {player && player.success
          ? renderPlayerData()
          : player
          ? player.errorMessage
          : null}
      </div>
      <div className="player-photo-box">
        <img
          src={player && player.success ? player.data.pictureURL : null}
          alt="Could not retrieve player's picture"
        ></img>
      </div>
    </div>
  );
}
