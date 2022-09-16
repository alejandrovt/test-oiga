import React, { useState, useEffect } from 'react';
//import { DaprClient } from '@dapr/dapr';

export const UserRegister = () => {
    const [users, setUsersState] = useState();
    const [value, setValueState] = useState('');

    useEffect(() => {}, []);

    useEffect(() => {}, [users]);

    const searchUsers = async (query) => {};

    const onClickSearch = (ev) => {
        searchUsers(value);
    };

    const onChangeSearch = (ev) => {
        setValueState(ev.target.value);
    };

    return (
        <>
            <div>Register</div>
            <input type='text' placeholder='Full name' />
            <input type='text' placeholder='User name' />
            <input type='text' placeholder='Phone' />
            <button type='button' onClick={onClickSearch}>
                Save
            </button>
        </>
    );
};

export default UserRegister;
