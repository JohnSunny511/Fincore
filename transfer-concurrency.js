
import http from 'k6/http';
import { check } from 'k6';
import { Counter } from 'k6/metrics';

const successfulWithdrawals = new Counter('successful_withdrawals');
const rejectedWithdrawals = new Counter('rejected_withdrawals');
const unexpectedResponses = new Counter('unexpected_responses');

export const options = {
    scenarios: {
        concurrent_withdrawals: {
            executor: 'per-vu-iterations',
            vus: 10,
            iterations: 1,
            maxDuration: '30s',
        },
    },
};

export default function () {

    const url = 'http://localhost:5298/Transaction/withdraw';

    const payload = JSON.stringify({
        account_id: 4,
        amount: Number(__ENV.AMOUNT),
        description: 'Concurrent withdrawal testing'
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${__ENV.TOKEN}`
        }
    };

    const response = http.post(url, payload, params);

    if (response.status === 200) {
        successfulWithdrawals.add(1);
    }
    else if (response.status === 409) {
        rejectedWithdrawals.add(1);
    }
    else {
        unexpectedResponses.add(1);
    }

    check(response, {
        'Response is successful or expected rejection':
            (r) => r.status === 200 || r.status === 409
    });

    console.log(
        `VU: ${__VU}, Status: ${response.status}`
    );
}
