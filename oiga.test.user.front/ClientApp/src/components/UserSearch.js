import React, { useState, useEffect } from 'react';
//import { DaprClient } from '@dapr/dapr';

import { UserRegister } from './UserRegister';

export const UserSearch = () => {
    const [users, setUsersState] = useState();
    const [value, setValueState] = useState('');

    useEffect(() => {
        searchUsers('');
    }, []);

    const searchUsers = async (query) => {
        const response = await fetch(`user/search?query=${query}`);
        const data = await response.json();
        setUsersState(data);
    };

    const onClickSearch = (ev) => {
        searchUsers(value);
    };

    const onChangeSearch = (ev) => {
        setValueState(ev.target.value);
    };

    return (
        <>
            <div>
                <h1 id='tabelLabel'>Users</h1>
                <input type='text' placeholder='Search' onChange={onChangeSearch} value={value} />
                <button type='button' onClick={onClickSearch}>
                    Search
                </button>
                <table className='table table-striped' aria-labelledby='tabelLabel'>
                    <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>User Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users ? (
                            <>
                                {users.length == 0 ? (
                                    <tr>
                                        <td colSpan={2}>No se encontraron resultados</td>
                                    </tr>
                                ) : (
                                    users.map((user) => (
                                        <tr key={user.id}>
                                            <td>{user.fullName}</td>
                                            <td>{user.userName}</td>
                                        </tr>
                                    ))
                                )}
                            </>
                        ) : (
                            <tr>
                                <td colSpan={2}>Loading</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <UserRegister />
            </div>
        </>
    );
};

export default UserSearch;
