# Instalock
**Instantly lock in your main agent in VALORANT**  
> **Note**
> Old program I wrote over a year ago but still working as of 4/1/23

### Features
- Uses the VALORANT API to login, no password required
- Little to no performance issues in terms of gameplay
- Locks in before the agent select screen even loads
- 99% success rate (unless someone is also using this program)

![](https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExNjc1N2NkZTE2YTc1YTJjZDBmYjNjZDMzYTNjMGZiNjJjMjcxZDc3MSZjdD1n/PJdmyX6DlUkvpVb9gq/giphy.gif)

> **Warning**
> Use at your own risk of course, I'm not responsible for any actions that Riot decide to take

#### appSettings.json
```jsonc
{
  "maps": { //map specific agents
    "ascent": "sage",
    "bind": "sage",
    "icebox": "sage",
    "haven": "sage",
    "fracture": "sage",
    "breeze": "sage",
    "split": "sage",
    "pearl": "sage",
    "lotus": "sage"
  },
  "region": "NA", //EU, KO, AP
  "check_interval": 50, //ms, how frequently to check if a match is found
  "static_delay": 0, //ms, how long to wait before locking in
  "pause_program_after_lock_in": true, //pausing program to prevent performance issues
  "launch_valorant_if_not_open": false
}
```
Using the API wrapper from [ValAPI.Net](https://github.com/brianbaldner/ValAPI.Net), it might be outdated though.
