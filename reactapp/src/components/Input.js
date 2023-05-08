import React, { useState, useRef } from 'react';
import useFetch from 'react-fetch-hook';


export default function Form() {
  const inputRef = useRef(null);
  const atomicNInput = useRef(null);

  const [equation, setEquation] = useState('');

 const data = useFetch('https://localhost:7249/api/PeriodicTable/nAtomico/4');

  const handleClick = () => {
    getData(inputRef.current.value);

  }
  async function getData(name) {
    const encoded = encodeURI('https://localhost:7249/api/PeriodicTable/balanceamento/' + name)

    await fetch(encoded, {
      method: 'get',
    })
      .then(res => { return res.text() })
      .then(
        (result) => {
          setEquation(result);
        }
      )
      .catch((err) => console.log(err))
  }
  return (
    <>
      <label>
        Equation
        <input
          type="text"
          ref={inputRef}
          name="equation"
        />
        <button onClick={handleClick}>
          Balance
        </button>
        <h2>Equacao {equation}</h2>
      </label>

    </>
  );
}
