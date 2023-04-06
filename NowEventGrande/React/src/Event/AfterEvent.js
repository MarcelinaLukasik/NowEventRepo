import React from "react";
import "../styles/rateSlider.css";
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import balloonsIcon from "../images/icons/colorful_balloons.png";

function AfterEvent() {
  const location = useLocation();
  const eventId = location.state.EventId;
  const [CommunicationRate, setCommunicationRate] = useState(3);
  const [QualityRate, setQualityRate] = useState(3);
  const [SpeedRate, setSpeedRate] = useState(3);
  const [isPosted, setIsPosted] = useState(false);

  useEffect(() => {
    checkIfRated();
  }, []);

  async function checkIfRated() {
    const res = await fetch(`/events/${eventId}/CheckIfRated`);
    if (res.ok) {
      setIsPosted(true);
    }
  }

  function moveSlider(evt) {
    const slider = evt.target;
    const value = slider.value;
    const rate = slider.id;
    if (rate === "CommunicationRange") {
      setCommunicationRate(value);
    } else if (rate === "QualityRange") {
      setQualityRate(value);
    } else if (rate === "SpeedRange") {
      setSpeedRate(value);
    }
  }

  function handleSubmit(evt) {
    evt.preventDefault();
    handlePost();
  }

  function handlePost() {
    async function fetchData() {
      const res = await fetch(`/events/SaveRatings`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          CommunicationRating: CommunicationRate,
          QualityRating: QualityRate,
          SpeedRating: SpeedRate,
          EventId: eventId,
        }),
      });
      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        throw new Error(message);
      } else {
        setIsPosted(true);
      }
    }
    fetchData();
  }

  return (
    <div className="event">
      <div>
        <h1>After event</h1>
        <div className="row">
          <div className="Event-col-12">
            <br />
            {isPosted && (
              <div>
                <h2>You have rated this event. Thank you.</h2>
                <img src={balloonsIcon} alt="img" className="bigIcon" />
              </div>
            )}
            {!isPosted && (
              <div>
                <h2>
                  Please take a moment to rate your experience
                  <br></br>
                  with the contractor:
                </h2>
                <div>
                  <h3>Communication:</h3>
                  <div className="rateSlidecontainer">
                    <input
                      type="range"
                      min="1"
                      max="5"
                      value={CommunicationRate}
                      className="rateSlider"
                      id="CommunicationRange"
                      onInput={moveSlider}
                    />
                  </div>
                  <h3>{CommunicationRate}</h3>
                </div>
                <div>
                  <h3>Quality:</h3>
                  <div className="rateSlidecontainer">
                    <input
                      type="range"
                      min="1"
                      max="5"
                      value={QualityRate}
                      className="rateSlider"
                      id="QualityRange"
                      onInput={moveSlider}
                    />
                  </div>
                  <h3>{QualityRate}</h3>
                </div>
                <div>
                  <h3>Speed:</h3>
                  <div className="rateSlidecontainer">
                    <input
                      type="range"
                      min="1"
                      max="5"
                      value={SpeedRate}
                      className="rateSlider"
                      id="SpeedRange"
                      onInput={moveSlider}
                    />
                  </div>
                  <h3>{SpeedRate}</h3>
                </div>
                <div>
                  <form onSubmit={handleSubmit}>
                    <input
                      className="saveGuest"
                      type="submit"
                      value="Send ratings"
                    />
                  </form>
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default AfterEvent;
