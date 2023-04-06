import React from "react";

const range = (start, end) => {
  return [...Array(end).keys()].map((el) => el + start);
};

export const Pagination = ({
  currentPage,
  totalPages,
  itemsFrom,
  itemsTo,
  setCurrentPage,
}) => {
  const pages = range(1, totalPages);

  return (
    <div>
      Pagination
      <br />
      <button
        disabled={currentPage === 1}
        onClick={() => setCurrentPage((prevState) => prevState - 1)}
      >
        Prev
      </button>
      <ul>
        <li>{range(1, 12)}</li>
      </ul>
      <p>{currentPage}</p>
      <button onClick={() => setCurrentPage((prevState) => prevState + 1)}>
        Next
      </button>
    </div>
  );
};
