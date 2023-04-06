import React from "react";
import "../styles/calendar.css";
import { Col } from "react-bootstrap";
import calendarIcon from "../images/icons/list.png";
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import Calendar from "react-calendar";
import SideBar from "./SideBar";
import Address from "./Address";
import ProgressBar from "./ProgressBar";

function Location() {
  const location = useLocation();
  const eventId = location.state.EventId;
  const [id, setEventId] = useState(eventId);
  const [date, setDate] = useState(new Date());
  const [res, setRes] = useState([]);
  const [timeOfDayStart, setTimeOfDayStart] = useState("AM");
  const [timeOfDayEnd, setTimeOfDayEnd] = useState("AM");
  const [startHour, setStartHour] = useState("00");
  const [startMinutes, setStartMinutes] = useState("00");
  const [endHour, setEndHour] = useState("00");
  const [endMinutes, setEndMinutes] = useState("00");
  const [isValid, setValid] = useState(true);
  const [buttonSubmit, setButtonSubmit] = useState("Save");
  const [fetchCurrentProgress, setFetchCurrentProgress] = useState(false);

  useEffect(() => {
    fetchRequest();
  }, []);

  async function fetchRequest(key) {
    const data = await fetch(`	https://api.adviceslip.com/advice`);
    const dataJ = await data.json();
    setRes(dataJ.slip.advice);
  }

  async function handleDateSave(evt) {
    evt.preventDefault();
    setButtonSubmit("Saving...");
    const res = await fetch(`/events/${id}/SaveDate`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "x-access-token": "token-value",
      },
      body: JSON.stringify({
        Date: date,
        TimeOfDayStart: timeOfDayStart,
        TimeOfDayEnd: timeOfDayEnd,
        StartHour: startHour,
        StartMinutes: startMinutes,
        EndHour: endHour,
        EndMinutes: endMinutes,
      }),
    });

    if (!res.ok) {
      const message = `An error has occured: ${res.status} - ${res.statusText}`;
      setValid(false);
      throw new Error(message);
    } else {
      setValid(true);
      setFetchCurrentProgress(true);
      setButtonSubmit("Save");
    }
  }

  return (
    <div className="event">
      <div className="row">
        <div className="Event-col-4"></div>
        <div className="Event-col-4">
          <ProgressBar
            fetchCurrentProgress={fetchCurrentProgress}
            setFetchCurrentProgress={setFetchCurrentProgress}
          />
        </div>
        <div className="Event-col-4"></div>
      </div>
      <div>
        <h1>Event venue and date</h1>
        <div className="row">
          <div className="Event-col-3">
            <div className="sidebar">
              <SideBar eventId={eventId} />
            </div>
          </div>
          <div className="Event-col-4">
            <br />
            <div className="calendar-container">
              <Calendar onChange={setDate} value={date} locale="en-GB" />
            </div>
            <br />
            <h3 className="text-center">
              <span className="bold">Selected Date:</span> {date.toDateString()}
            </h3>
            <div>
              {!isValid && (
                <p className="wrongInputMessage">
                  Please provide valid date and time!
                </p>
              )}
            </div>
          </div>
          <div className="Event-col-3">
            <div className="clockContainer">
              <h3 className="clockTitle">Start:</h3>
              <div className="clock">
                <input
                  type="number"
                  value={startHour}
                  min="1"
                  max="12"
                  required
                  onChange={(e) => setStartHour(e.target.value)}
                />
                <p>:</p>
                <input
                  type="number"
                  value={startMinutes}
                  min="0"
                  max="59"
                  required
                  onChange={(e) => setStartMinutes(e.target.value)}
                />
                <div className="AMPM">
                  <label className="clockLabel">
                    <input
                      type="radio"
                      className="AM"
                      name="start"
                      value="AM"
                      defaultChecked
                      onClick={(e) => setTimeOfDayStart(e.target.value)}
                    />
                    <span>AM</span>
                  </label>
                  <label className="clockLabel">
                    <input
                      type="radio"
                      className="PM"
                      name="start"
                      value="PM"
                      onClick={(e) => setTimeOfDayStart(e.target.value)}
                    />
                    <span>PM</span>
                  </label>
                </div>
              </div>
              <h3 className="clockTitle">End:</h3>
              <div className="clock">
                <input
                  type="number"
                  value={endHour}
                  min="1"
                  max="12"
                  required
                  onChange={(e) => setEndHour(e.target.value)}
                />
                <p>:</p>
                <input
                  type="number"
                  value={endMinutes}
                  min="0"
                  max="59"
                  required
                  onChange={(e) => setEndMinutes(e.target.value)}
                />
                <div className="AMPM">
                  <label className="clockLabel">
                    <input
                      type="radio"
                      className="AM"
                      name="end"
                      value="AM"
                      defaultChecked
                      onClick={(e) => setTimeOfDayEnd(e.target.value)}
                    />
                    <span>AM</span>
                  </label>
                  <label className="clockLabel">
                    <input
                      type="radio"
                      className="PM"
                      name="end"
                      value="PM"
                      onClick={(e) => setTimeOfDayEnd(e.target.value)}
                    />
                    <span>PM</span>
                  </label>
                </div>
              </div>
            </div>
          </div>
          <div className="Event-col-2">
            <Col cs={12} md={6} xl={6}>
              <img src={calendarIcon} alt="img" className="featureIcon" />
            </Col>
          </div>
        </div>
      </div>
      <div className="row">
        <button
          type="button"
          className="saveDate"
          value="Save date"
          onClick={handleDateSave}
        >
          <span>{buttonSubmit}</span>
        </button>
      </div>
      <div className="row">
        <Address
          eventId={eventId}
          setFetchCurrentProgress={setFetchCurrentProgress}
        />
      </div>
    </div>
  );
}

export default Location;
