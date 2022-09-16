import React, { useState, useEffect } from 'react';
import axios from 'axios';
import InfiniteScroll from 'react-infinite-scroll-component';

import { UserRegister } from './UserRegister';
import { UserDetail } from './UserDetail';

export const UserPage = () => {
    const [users, setUsersState] = useState([]);
    const [userId, setUserIdState] = useState();
    const [query, setQueyState] = useState('');
    const [page, setPageState] = useState(1);
    const [hasMore, setHasMoreState] = useState(true);

    useEffect(() => {
        if (page && page > 1) {
            searchUsers(page);
        }
    }, [page]);

    const onClickSearch = (ev) => {
        setPageState(1);
        setUsersState([]);
        setHasMoreState(true);
        searchUsers(page);
    };

    const onChangeSearch = (ev) => {
        setQueyState(ev.target.value);
    };

    const onNext = () => {
        const vPage = page + 1;
        setPageState(vPage);
    };

    const onClickDetailUser = (pId) => {
        setUserIdState(pId);
    };

    const searchUsers = (pPage) => {
        axios.get(`user/search?page=${pPage}&query=${query}`).then((response) => {
            if (response.data.length === 0) {
                setHasMoreState(false);
            } else {
                const usersT = users.concat(response.data);
                setUsersState(usersT);
            }
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
            <div id='scrollableDiv' style={{ height: '300px', overflow: 'scroll' }}>
                <table className='table table-striped' aria-labelledby='tabelLabel'>
                    <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>User Name</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {users && (
                            <>
                                {users.length === 0 ? (
                                    <tr>
                                        <td colSpan={2}>No se encontraron resultados</td>
                                    </tr>
                                ) : (
                                    <InfiniteScroll dataLength={users.length} next={onNext} hasMore={hasMore} loader={<h4>Loading...</h4>} scrollableTarget='scrollableDiv' endMessage={<h4>End Results</h4>}>
                                        {users.map((user) => (
                                            <tr key={user.id}>
                                                <td>{user.fullName}</td>
                                                <td>{user.userName}</td>
                                                <td>
                                                    <button type='button' onClick={() => onClickDetailUser(user.id)}>
                                                        view
                                                    </button>
                                                </td>
                                            </tr>
                                        ))}
                                    </InfiniteScroll>
                                )}
                            </>
                        )}
                    </tbody>
                </table>
            </div>

            <UserDetail userId={userId} />
        </div>
    );
};

export default UserPage;
