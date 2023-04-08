import SideBar from "../Utils/SideBar";
import ProgressBar from "../Progress/ProgressBar";
import React from "react";
import { useLocation } from "react-router-dom";
import { Col } from "react-bootstrap";
import calendarIcon from "../../images/icons/salary.png";
import { useState, useEffect, setErrors } from "react";
import "../../styles/event.css";
import "../../styles/budget.css";


function Budget() {
  const location = useLocation();
  const eventId = location.state.EventId;
  const [rent, setRent] = useState("");
  const [decoration, setDecoration] = useState("");
  const [food, setFood] = useState("");
  const [budget, setBudget] = useState([]);
  const [isValid, setValid] = useState(true);
  const [fetchCurrentProgress, setFetchCurrentProgress] = useState(false);
  const [open, setOpen] = useState({
    isRentOpen: false,
    isDecorOpen: false,
    isFoodOpen: false,
  });
  const openInput = (budgetOption) => {
    setOpen({ ...open, [budgetOption.IsOpenId]: !budgetOption.IsOpen });
  };

  const budgetOptions = [
    {
      Name: "Rental cost",
      Id: "RentPrice",
      IsOpen: open.isRentOpen,
      IsOpenId: "isRentOpen",
      Value: rent,
      SetAction: setRent,
      Price: budget.rentPrice,
    },
    {
      Name: "Decoration cost",
      Id: "DecorationPrice",
      IsOpen: open.isDecorOpen,
      IsOpenId: "isDecorOpen",
      Value: decoration,
      SetAction: setDecoration,
      Price: budget.decorationPrice,
    },
    {
      Name: "Food cost",
      Id: "FoodPrice",
      IsOpen: open.isFoodOpen,
      IsOpenId: "isFoodOpen",
      Value: food,
      SetAction: setFood,
      Price: budget.foodPrice,
    },
  ];

  useEffect(() => {
    fetchStatsData();
  }, []);

  async function handleSubmit(evt, budgetOption) {
    evt.preventDefault();
    handlePatch(budgetOption.Id, budgetOption.Value.toString()).then(() => {
      fetchStatsData()
        .then(() => {
          openInput(budgetOption);
        })
        .then(() => {
          setFetchCurrentProgress(true);
        });
    });
  }

  async function fetchStatsData() {
    const res = await fetch(`/budget/${eventId}/GetBudget`);
    res
      .json()
      .then((res) => setBudget(res))
      .then()
      .catch((err) => setErrors(err));
  }

  async function handlePatch(typeToChange, valueToChange) {
    const res = await fetch(`/budget/${eventId}/Update/${typeToChange}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(valueToChange),
    });
    if (!res.ok) {
      const message = `An error has occured: ${res.status} - ${res.statusText}`;
      setValid(false);
      throw new Error(message);
    } else {
      setValid(true);
    }
    const result = await res;
    return result;
  }

  return (
    <div className="event">
      <div className="row">
        <div className="Event-col-12">
          <ProgressBar
            fetchCurrentProgress={fetchCurrentProgress}
            setFetchCurrentProgress={setFetchCurrentProgress}
          />
        </div>
      </div>
      <h1>Budget</h1>
      <div className="row">
        <div className="Event-col-3">
          <div className="sidebar">
            <SideBar eventId={eventId} />
          </div>
        </div>
        <div className="Event-col-5">
          <br />
          {Array.from(budgetOptions).map((budgetOption, i) => {
            return (
              <form
                onSubmit={(e) => handleSubmit(e, budgetOption)}
                className="budgetForm"
                id={budgetOption.Id}
                key={i}
              >
                <div>
                  {!budgetOption.IsOpen && (
                    <div>
                      <label>{budgetOption.Name}: </label>
                      <label>{budgetOption.Price}$</label>
                      <input
                        className="editCost"
                        type="button"
                        value="Edit"
                        onClick={() => openInput(budgetOption)}
                        isOpen={budgetOption.IsOpen}
                      />
                    </div>
                  )}
                  {budgetOption.IsOpen && (
                    <div>
                      <label>{budgetOption.Name}:</label>
                      <input
                        className="costInput"
                        type="text"
                        value={budgetOption.Value}
                        required
                        onChange={(e) => budgetOption.SetAction(e.target.value)}
                      />
                      <input className="saveCosts" type="submit" value="Save" />
                    </div>
                  )}
                </div>
              </form>
            );
          })}
          <div>
            {!isValid && (
              <p className="wrongInputMessage">Please provide valid numbers!</p>
            )}
          </div>
        </div>
        <div className="Event-col-2">
          <h3>Total cost: {budget.total}</h3>
        </div>
        <div className="Event-col-2">
          <Col cs={12} md={6} xl={6}>
            <img src={calendarIcon} alt="img" className="featureIcon" />
          </Col>
        </div>
      </div>
    </div>
  );
}

export default Budget;
