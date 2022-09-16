import React, { useState, useEffect } from 'react';
import axios from 'axios';

import { UserRegister } from './UserRegister';

export const UserPage = () => {
    const [users, setUsersState] = useState();
    const [query, setQueyState] = useState('');
    const [page, setPageState] = useState(1);

    useEffect(() => {
        if (page) {
            searchUsers(page);
        }
    }, [page]);

    const onClickSearch = (ev) => {
        searchUsers(page);
    };

    const onChangeSearch = (ev) => {
        setQueyState(ev.target.value);
    };

    const onClickPrevoious = (ev) => {
        if (page <= 1) {
            return;
        }
        const vPage = page - 1;
        setPageState(vPage);
    };

    const onClickNext = (ev) => {
        const vPage = page + 1;
        setPageState(vPage);
    };

    const searchUsers = (pPage) => {
        console.log(pPage);
        axios.get(`user/search?page=${pPage}&query=${query}`).then((response) => {
            setUsersState(response.data);
        });
    };

    return (
        <div>
            <h1 id='tabelLabel'>User</h1>
            <hr />
            <UserRegister searchUsers={searchUsers} />
            <hr />
            <h4 id='tabelLabel'>Search</h4>
            <input type='text' placeholder='Search' onChange={onChangeSearch} value={query} />
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
            <div>
                <button type='button' onClick={onClickPrevoious} style={{ width: '100px' }}>
                    previous
                </button>
                &nbsp;&nbsp;&nbsp;
                <button type='button' onClick={onClickNext} style={{ width: '100px' }}>
                    next
                </button>
            </div>
        </div>
    );
};

export default UserPage;
