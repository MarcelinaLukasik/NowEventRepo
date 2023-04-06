import React, { useState, useEffect } from "react";
import { Trash } from "react-bootstrap-icons";
import Modal from "react-bootstrap/Modal";
import AddGuestForm from "./AddGuestForm";

const AllGuests = ({
  eventId,
  addChecklistCount,
  subtractChecklistCount,
  setFetchCurrentProgress,
}) => {
  const [hasError, setErrors] = useState(false);
  const [guests, setState] = useState([]);
  const [guestCount, setGuestCount] = useState(0);
  const [id, setId] = useState("");
  const [show, setShow] = useState(false);
  const [value, setValue] = useState("Default");
  const handleClose = () => setShow(false);
  const currentEventId = eventId;
  const [isOpen, setOpen] = useState(false);
  const openInput = () => {
    setOpen(!isOpen);
  };

  const addGuestCount = () => {
    setGuestCount(guestCount + 1);
  };

  useEffect(() => {
    async function fetchData() {
      const res = await fetch(`/guest/${currentEventId}/all`);
      res
        .json()
        .then((res) => setState(res))
        .catch((err) => setErrors(err));
    }
    fetchData();
  }, []);

  useEffect(() => {
    async function fetchData() {
      const res = await fetch(`/guest/${currentEventId}/all`);
      res
        .json()
        .then((res) => setState(res))
        .catch((err) => setErrors(err));
    }
    fetchData();
  }, [guestCount]);

  function removeGuest(evt) {
    evt.preventDefault();
    handlePost();
    handleClose();
  }

  function handleRemoveModal(event) {
    let id = event.currentTarget.id;
    setId(id);
    setShow(true);
  }

  async function handleSort(sortType) {
    const response = await fetch(`/guest/${currentEventId}/all/${sortType}`);
    const result = await response.json();
    setState(result);
  }

  function handleSortSubmit(evt) {
    evt.preventDefault();
    if (evt.target.value !== "Default") {
      handleSort(evt.target.value);
      setValue(evt.target.value);
    } else {
      setValue(evt.target.value);
    }
  }

  async function handlePost() {
    async function fetchData() {
      const res = await fetch(`/guest/removeGuest/${id}`, {
        method: "delete",
        headers: {
          "Content-Type": "application/json",
          "x-access-token": "token-value",
        },
        body: JSON.stringify({ Id: id }),
      });
      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        throw new Error(message);
      }
      const result = await res.json();
      setGuestCount(guestCount - 1);
      subtractChecklistCount();
      setFetchCurrentProgress(true);
    }
    fetchData();
  }

  return (
    <div>
      <div className="row">
        <div className="Event-col-6">
          <div className="selectSort">
            <label>Sort by:</label>
            <select
              className="sortGuests"
              value={value}
              onChange={handleSortSubmit}
            >
              <option value="Default">Default</option>
              <option value="ascending">Ascending</option>
              <option value="descending">Descending</option>
            </select>
          </div>
          <h2>Current guests on list:</h2>
          {Array.from(guests).map((item, i) => (
            <div className="guestInfo" key={i}>
              {item.quantityNeeded} {item.firstName + " " + item.lastName}
              <button
                className="removeButton"
                id={item.id}
                type="submit"
                value="Remove"
                onClick={handleRemoveModal}
              >
                <Trash color="white" />
              </button>
              <hr className="solid"></hr>
            </div>
          ))}
          <Modal show={show} onHide={handleClose} className="removeModal">
            <Modal.Header>
              <Modal.Title>Remove confirmation</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              Are you sure you want to remove this guest from list?
            </Modal.Body>
            <Modal.Footer>
              <div className="removeButtons">
                <button
                  className="rejectButton"
                  variant="secondary"
                  onClick={handleClose}
                >
                  No
                </button>
                <button
                  className="acceptButton"
                  variant="primary"
                  onClick={removeGuest}
                >
                  Yes
                </button>
              </div>
            </Modal.Footer>
          </Modal>
          <h3>Total guests: {Array.from(guests).length} </h3>
        </div>
        <div className="Event-col-5">
          <AddGuestForm
            onClick={openInput}
            addGuestCount={addGuestCount}
            isOpen={isOpen}
            eventId={currentEventId}
            addChecklistCount={addChecklistCount}
            setFetchCurrentProgress={setFetchCurrentProgress}
          />
        </div>
      </div>
    </div>
  );
};
export default AllGuests;
